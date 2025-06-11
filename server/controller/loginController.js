const loginService = require('../service/loginService');

exports.Login = async (req, res) => {
    try {
        const { userId, password } = req.body; // 클라이언트에서 body로 받는다고 가정

        const loginInfo = await loginService.Login(userId, password);

        if (!loginInfo.isLogin) {
            return res.status(401).json({ message: loginInfo.reason });
        }

        res.status(200).json({
            message: '로그인 성공',
            data: loginInfo
        });
        return res;
    } catch (err) {
        console.error(err);
        res.status(500).json({ message: '서버 오류' });
    }
};
