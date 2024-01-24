module.exports = app => {
  const saves = require('../controllers/saves.controller');

  var router = require("express").Router();

  // Create a new save
  router.post("/", saves.create);

  // Retrieve all saves
  router.get("/", saves.findAll);

  // Retrieve a single save with savename
  router.get("/:savename", saves.findOne);

  // Update a save with savename
  router.put("/:savename", saves.update);

  // Delete a save with savename
  router.delete("/:savename", saves.delete);

  app.use("/api/saves", router);
}