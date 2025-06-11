const mysql = require('mysql2/promise');

const db = mysql.createPool({
    host: 'localhost',
    user: 'root',
    password: '0000',
    database: 'gameserver',
});

module.exports = db;