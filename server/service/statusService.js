const db = require('../models/db'); // DB 커넥션 (promise 기반)

exports.GetStats = async (userSeq) => {
    try {
        const [rows] = await db.query(`
      SELECT user_seq, stat_id, value 
      FROM user_stat 
      WHERE user_seq = ?
    `, [userSeq]);

        const result = {
            user_seq: userSeq,
            stats: rows.map(row => ({
                stat_id: row.stat_id,
                stat_value: row.value
            }))
        };
        return result;
    } catch (err) {
        console.error('[GetUserStats] DB 오류:', err);
        throw err;
    }
}

exports.GetItemStats = async (userSeq) => {
    try {
        const [rows] = await db.query(`
      Select stat_id, total_value as value from
     (SELECT * FROM user_equipment WHERE user_seq = ?) ue
         LEFT JOIN equipment_total_stat ets on ue.item_uid = ets.item_uid
    `, [userSeq]);

        const result = {
            user_seq: userSeq,
            stats: rows.map(row => ({
                stat_id: row.stat_id,
                stat_value: row.value
            }))
        };
        return result;
    } catch (err) {
        console.error('[GetUserStats] DB 오류:', err);
        throw err;
    }
}