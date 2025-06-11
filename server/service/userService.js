const db = require('../models/db'); // DB 연결 예시

exports.findUserById = async (id) => {
    const [rows] = await db.query('SELECT * FROM user WHERE id = ?', [id]);
    return rows[0];
};
