const express = require("express");
const mongoose = require("mongoose");
const app = express();
const api = require("./api");
const port = 4000;

app.use("/api", api);

mongoose.connect("localhost/app");
mongoose.connection.once("open", () => {
    console.log("Connected to database");
    app.listen(port, function() {
        console.log("Web server started on port " + port);
    });
})

