const userService = require('../service/userService');

exports.getUserById = async (req, res) => {
    try {
        const user = await userService.findUserById(req.params.id);
        if (!user) {
            return res.status(404).json({ message: '사용자를 찾을 수 없습니다.' });
        }
        res.json(user);
    } catch (err) {
        console.error(err);
        res.status(500).json({ message: '서버 오류' });
    }
};