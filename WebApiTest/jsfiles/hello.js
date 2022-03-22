module.exports = function (callback, name) {
    var msg = "Hello " + name;
    callback(null, msg);
};