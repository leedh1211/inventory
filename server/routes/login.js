const express = require('express');
const router = express.Router();
const loginController = require('../controller/loginController');

router.post('/onLogin',  async (req, res) => {
    try {
        const loginInfo = await loginController.Login(req, res);
        res.json({ success: true, result : loginInfo});
    } catch (err) {
        console.error(err);
        res.status(500).json({ success: false, error: 'Internal Error' });
    }
});



module.exports = router;