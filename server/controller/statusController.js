const statusService = require('../service/statusService');

exports.GetStats = async (req, res) => {
    try {
        const { userSeq } = req.body; // 클라이언트에서 body로 받는다고 가정

        const status = await statusService.GetStats(userSeq);

        res.status(200).json(status);
        return res;
    } catch (err) {
        console.error(err);
        res.status(500).json({ message: '서버 오류' });
    }
};

exports.GetItemStats = async (req, res) => {
    try {
        const { userSeq } = req.body; // 클라이언트에서 body로 받는다고 가정

        const status = await statusService.GetItemStats(userSeq);

        res.status(200).json(status);
        return res;
    } catch (err) {
        console.error(err);
        res.status(500).json({ message: '서버 오류' });
    }
};
