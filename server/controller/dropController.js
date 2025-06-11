const dropService = require('../service/dropService');

exports.handleMonsterDrop = async ({ monsterId, userSeq, dropRateModifier = 1.0 }) => {
    return await dropService.generateMonsterDrop(monsterId, userSeq, dropRateModifier);
};