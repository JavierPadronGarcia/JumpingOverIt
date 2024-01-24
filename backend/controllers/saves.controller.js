const db = require("../models");
const Saves = db.saves;
const Op = db.Sequelize.Op;

exports.create = (req, res) => {
  if (!req.body.saveName || !req.body.actualLevel) {
    req.satus(400).send({
      message: "Content cannot be empty!"
    });
  }

  const save = {
    saveName: req.body.saveName,
    actualLevel: req.body.actualLevel
  }

  Saves.create(save).then(data => {
    res.send(data);
  }).catch(err => {
    res.status(500).send({
      message: err.message || "Some error occurred while creating the save"
    });
  })
}

exports.findAll = (req, res) => {
  Saves.findAll().then(data => {
    res.send(data);
  }).catch(err => {
    res.status(500).send({
      message: err.mesage || "Some error occurred while retrieving all saves"
    })
  })
}

exports.findOne = (req, res) => {
  if (!req.params.saveName) {
    req.satus(400).send({
      message: "Content cannot be empty!"
    });
  }

  Saves.findByPk(req.params.saveName).then(save => {
    req.send(save);
  }).catch(err => {
    req.status(500).send({
      message: err.message || "Some error ocurred while retrieving a save"
    })
  })
}
exports.update = (req, res) => {
  if (!req.body.actualLevel) {
    req.satus(400).send({
      message: "Content cannot be empty!"
    });
  }

  const saveName = req.params.saveName;

  Saves.update({ saveName: saveName, actualLevel: req.body.actualLevel },
    { where: { saveName: saveName } }).then(data => {
      res.send(data)
    }).catch(err => {
      req.status(500).send({
        message: err.message || "Some error occurred while updating a save"
      })
    });
}

exports.delete = (req, res) => {
  const saveName = req.params.saveName;

  Saves.destroy({ where: { saveName: saveName } }).then(() => {
    req.status(200).send({
      message: "Save deleted!"
    })
  }).catch(err => {
    res.status(500).send({
      message: err.message || "Some error while deleting the save with saveName " + saveName
    })
  })
}