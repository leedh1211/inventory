const express = require('express');
const router = express.Router();
const statusController = require('../controller/statusController');

router.post('/get',  async (req, res) => {
    try {
        const status = await statusController.GetStats(req, res);
        res.json({ success: true, result : status});
    } catch (err) {
        console.error(err);
        res.status(500).json({ success: false, error: 'Internal Error' });
    }
});

router.post('/GetItemStat',  async (req, res) => {
    try {
        const status = await statusController.GetItemStats(req, res);
        res.json({ success: true, result : status});
    } catch (err) {
        console.error(err);
        res.status(500).json({ success: false, error: 'Internal Error' });
    }
});



module.exports = router;