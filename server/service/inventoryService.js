const db = require('../models/db');
const { v4: uuidv4 } = require('uuid');

exports.AddInventory = async (itemSeq, userSeq) => {
    const conn = await db.getConnection();
    try {
        const [[itemInfo]] = await conn.query(
            'SELECT is_stackable, category FROM item_master WHERE seq = ?',
            [itemSeq]
        );

        const itemUid = await makeUid(conn, itemSeq);
        var result = false;
        if (itemInfo.is_stackable) {
            const [[existingSlot]] = await conn.query(
                `SELECT slot_index, quantity
                 FROM user_inventory
                 WHERE user_seq = ? AND item_seq = ?`,
                [userSeq, itemSeq]
            );

            if (existingSlot) {
                result = await conn.query(
                    `UPDATE user_inventory
                     SET quantity = quantity + 1
                     WHERE user_seq = ? AND slot_index = ?`,
                    [userSeq, existingSlot.slot_index]
                );
            } else {
                result = await conn.query(
                    `INSERT INTO user_inventory (user_seq, slot_index, item_seq, quantity)
                    VALUES (?, 
                    (SELECT * FROM (
                        SELECT IFNULL(MAX(slot_index), -1) + 1 
                        FROM user_inventory 
                        WHERE user_seq = ?
                    ) AS subquery),
         ?, 1)`,
                    [userSeq, userSeq, itemSeq]
                );
            }
        } else {
            result = await conn.query(
                `INSERT INTO user_inventory (user_seq, slot_index, item_seq, quantity, item_uid)
            VALUES (?, 
            (SELECT * FROM (
             SELECT IFNULL(MAX(slot_index), -1) + 1 
             FROM user_inventory 
             WHERE user_seq = ?
         ) AS subquery),
         ?, ?, ?)`,
                [userSeq, userSeq, itemSeq, 1, itemUid]
            );
        }

        return result
    } catch (err) {
        console.error(err);
        throw err;
    } finally {
        conn.release(); // 연결 반환
    }
};

exports.GetInventory = async (seq) => {
    const [rows] = await db.query(`
    SELECT ui.user_seq, ui.slot_index, ui.item_uid, ui.item_seq, ui.quantity,
       CASE 
        WHEN ue.item_uid IS NOT NULL THEN true ELSE false
        END AS is_equipped
        FROM
            user_inventory ui
        LEFT JOIN
            user_equipment ue
            ON ui.item_uid = ue.item_uid
        WHERE
            ui.user_seq = ?
    `, [seq]);
    return rows;
}

const makeUid = async (conn, itemSeq) => {
    try {
        const [[row]] = await conn.query(
            `SELECT IC.is_uid FROM item_master IM
             LEFT JOIN item_category IC ON IC.seq = IM.category
             WHERE IM.seq = ?`, [itemSeq]
        );

        if (row?.is_uid) {
            const itemUid = uuidv4();
            await conn.query(
                'INSERT INTO equipment_instance (item_uid, item_seq) VALUES (?, ?)',
                [itemUid, itemSeq]
            );
            return itemUid;
        } else {
            return null;
        }
    } catch (err) {
        console.error('makeUid error:', err);
        throw err;
    }
};
