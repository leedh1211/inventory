const inventoryService = require('../service/inventoryService');

exports.AddInventory = async (req, res) => {
    const { itemSeq, userSeq } = req.body;
    try {
        const isAdd = await inventoryService.AddInventory(itemSeq, userSeq);
        if (!isAdd) {
            return res.status(404).json({ message: '아이템이 인벤토리에 추가되지못했습니다.' });
        }
        res.json(isAdd);
    } catch (err) {
        console.error(err);
        res.status(500).json({ message: '서버 오류' });
    }
};

exports.GetInventory = async (req, res) => {
    const { userSeq } = req.body;
    try {
        const inventoryList = await inventoryService.GetInventory(userSeq);
        res.json(inventoryList);
    } catch (err) {
        console.error(err);
        res.status(500).json({ message: '서버 오류' });
    }
};