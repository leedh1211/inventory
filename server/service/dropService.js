// services/dropService.js
const db = require('../models/db'); // mysql2/promise 풀 연결

exports.generateMonsterDrop = async (monsterId, userSeq, dropRateModifier) => {
    const conn = await db.getConnection();
    try {
        // 1. 몬스터 드랍 테이블 조회
        const [dropRows] = await conn.query(
            'SELECT item_seq, drop_rate FROM monster_drop_table WHERE monster_id = ?',
            [monsterId]
        );

        const droppedItems = [];

        // 2. 드랍 확률 계산
        for (const row of dropRows) {
            const rate = row.drop_rate * dropRateModifier;
            if (Math.random() < rate) {
                droppedItems.push(row.item_seq);
            }
        }

        // 만약 드랍되지않고 바로 인벤토리에 넣어야 할 시 이 시점에 인벤토리에 넣기
        return droppedItems;
    } finally {
        conn.release();
    }
};