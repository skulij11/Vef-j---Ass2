const mongoose = require("mongoose");

const UsersSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true   
    },
    token: String,
    gender: {
        type: String,
        required: true,
        validate: {
            validator: function(value) {
                return (value === "m" || value === "f" || value === "o");
            }
        }    
    }
},{versionKey: false});

const CompaniesSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    punchCount: {
        type: Number,
        required: true
    },
    description: {
        type: String
    }
},{versionKey: false})

const PunchesSchema = new mongoose.Schema({
    user_id: String,
    company_id: String,
    created: {
        type: Date,
        default: Date.now()
    },
    used: {
        type: Boolean,
        default: false
    }
},{versionKey: false});

const UserEntity = mongoose.model("Users", UsersSchema);
const CompanyEntity = mongoose.model("Companies", CompaniesSchema);
const PunchEntity = mongoose.model("Punches", PunchesSchema);

const entities = {
    User: UserEntity,
    Company: CompanyEntity,
    Punch: PunchEntity
};

module.exports = entities;