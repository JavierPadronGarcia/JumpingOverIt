module.exports = (sequelize, Sequelize) => {
  const Save = sequelize.define("saves", {
    saveName: {
      type: Sequelize.STRING,
      primaryKey: true,
    },
    actualLevel: {
      type: Sequelize.INTEGER
    }
  }, {
    timestamps: false,
  });

  return Save;
}