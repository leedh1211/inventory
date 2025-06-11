const express = require('express');
const router = express.Router();
const dropController = require('../controller/dropController');

router.post('/drop', async (req, res) => {
    const { monsterId, userSeq, dropRateModifier } = req.body;
    try {
        const items = await dropController.handleMonsterDrop({ monsterId, userSeq, dropRateModifier });
        res.json({ success: true, droppedItems: items });
    } catch (err) {
        console.error(err);
        res.status(500).json({ success: false, error: 'Internal Error' });
    }
});

module.exports = router;