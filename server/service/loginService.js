const db = require('../models/db'); // DB 커넥션 (promise 기반)

exports.Login = async (id, hashedPass) => {
    try {
        // 유저 조회
        const [rows] = await db.query('SELECT * FROM user WHERE id = ?', [id]);

        if (rows.length === 0) {
            return { isLogin: false, reason: '존재하지 않는 아이디입니다.' };
        }

        const user = rows[0];

        // 비밀번호 해시 비교
        if (user.password !== hashedPass) {
            return { isLogin: false, reason: '비밀번호가 일치하지 않습니다.' };
        }

        // 로그인 성공
        return {
            seq : user.seq,
            isLogin: true,
            id: user.id,
            name: user.name,
            gold: user.gold,
            level: user.level,
            current_exp : user.current_exp
            // 필요 시 토큰 등도 생성 가능
        };
    } catch (error) {
        console.error('[LoginService] 오류:', error);
        throw error; // 컨트롤러에서 처리
    }
};