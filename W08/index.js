const express = require('express');
const bodyparser = require('body-parser');
const app = express();

app.use(bodyparser.json());

// Array to store all companies
const companies = [];
// Array to store all users
const users = [];
// Array storing all users that have received punches from companies and the number of punches
const userPunches = [];

var userCount = 0;
var compCount = 0;

app.get("/api/companies", (req, res) => {
    res.json(companies);
});

app.get("/api/users", (req, res) => {
    res.json(users);
});

app.get("/api/companies/:id", (req, res) => {
    var id = parseInt(req.params.id);
    if(isNaN(id) || id >= companies.length || id < 0) {
        res.statusCode = 404;
        res.send("Company ID not valid")
        return;
    }
    res.json(companies[id]);
});

app.post("/api/companies", (req, res) => {
    var newCompany = req.body;
    //console.log(req.body);
    if(newCompany.name && newCompany.punchCount) {
        newCompany.id = compCount++;
        companies.push(newCompany);
        res.statusCode = 201;
        res.json(newCompany);
    }
    else  {
        res.statusCode = 400;
        res.send("Company not valid");
    }    
});

app.post("/api/users", (req, res) => {
    var newUser = req.body;
    //console.log(req.body);

    if(newUser.name && newUser.email) {
        newUser.id = userCount++;
        users.push(newUser);
        res.statusCode = 201;
        res.json(newUser);
    }
    else {
        res.statusCode = 400;
        res.send("User not valid");
    }    
});

app.get("/api/users/:id/punches", (req, res) => {

    var id = parseInt(req.params.id);
    // Check if user ID is valid
    if(id >= users.length || id < 0) {
        res.statusCode = 404;
        res.send("User ID not valid");
        return;
    }
    var user = users[id];
    //console.log(user);
    
    var resultArray = [];

    // We find all punches this user has received (if any) and store in resultArray
    for(var i = 0; i < userPunches.length; i++) {
        if(userPunches[i].userId === id) {
            resultArray.push(userPunches[i]);            
        }
    }
    // We filter the resultArray using ?company={id} query, store in another array (queryArray) and return it (if id is valid)
    if(req.query.company) {
        var queryArray = [];
        var queryID = parseInt(req.query.company);

        if(isNaN(queryID) || queryID >= companies.length ||queryID < 0) {
            res.statusCode = 404;
            res.send("Query parameter not valid");
            return;
        }
        for(var i = 0; i < resultArray.length; i++) {
            if(resultArray[i].compId === queryID) {
                queryArray.push(resultArray[i]);
            }
        }
        //console.log("QueryArray:", queryArray);
        res.json(queryArray);
        return;
    }    
    //console.log("ResultArray:", resultArray);
    res.json(resultArray);
});


app.post("/api/users/:id/punches", (req, res) => {
    
    var id = parseInt(req.params.id);
    // Check if user ID is valid
    if(id >= users.length || id < 0) {
        res.statusCode = 404;
        res.send("User ID not valid");
        return;
    }
    var user = users[id];
    //console.log(user);

    // Check if company id is in the body
    var comp = req.body;
    if(comp.id == null || comp.id == undefined) {
        res.statusCode = 400;
        res.send("Company ID needed")
        return;
    }
    var compId = parseInt(comp.id);
    //console.log(compId);

    // Check if id from body is valdid
    if(isNaN(compId) || compId >= companies.length || compId < 0) {
        res.statuscode = 404;
        res.send("Company ID not valid")
        return;
    }
    var company = companies[compId];
    //console.log(company);

    var date = new Date;

    // Object for a user, a company and number of punches
    var userPunch = {
        userId: user.id,
        compId: company.id,
        compName: company.name,
        punches: 1, 
        created: date.toJSON()
    };
    //console.log(userPunch);

    for(var i = 0; i < userPunches.length; i++) {
        // If this user already has a punch from this company we increase the punch count by 1
        if(userPunches[i].userId == userPunch.userId && userPunches[i].compId == userPunch.compId) {
            userPunches[i].punches++;
            res.json(userPunches[i]);
            return;
        }
    }
    // If the user hasn't had any punch from this company yet, we push the userPunch object to the array
    userPunches.push(userPunch);
    res.json(userPunch); 
});

app.listen(process.env.PORT || 5000);