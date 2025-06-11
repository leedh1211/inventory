const express = require('express');
const router = express.Router();
const inventoryController = require('../controller/inventoryController');

router.post('/Add', async (req, res) => {
    try {
        const items = await inventoryController.AddInventory(req, res);
        res.json({ success: true, AddItem: items });
    } catch (err) {
        console.error(err);
        res.status(500).json({ success: false, error: 'Internal Error' });
    }
});

router.post('/Get', async (req, res) => {
    try {
        const items = await inventoryController.GetInventory(req, res);
        res.json({ success: true, ItemList: items });
    } catch (err) {
        console.error(err);
        res.status(500).json({ success: false, error: 'Internal Error' });
    }
});


module.exports = router;