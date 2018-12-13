var page = require('webpage').create();
var system = require('system');
var args = system.args;

page.settings.loadImages = false;
page.settings.webSecurityEnabled = false;
page.settings.resourceTimeout = 1000;
page.settings.userAgent = 'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.120 Safari/537.36';

page.open(args[1], function (status) {
    if (status === "success") {
        console.log(page.content);
    }
    else {
        console.log("ERROR");
    }
    phantom.exit();
});