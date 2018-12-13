var page = require('webpage').create();
var system = require('system');
var args = system.args;

page.settings.loadImages = false;
page.settings.webSecurityEnabled = false;
page.settings.resourceTimeout = 1000;
page.settings.userAgent = 'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.120 Safari/537.36';
page.onConsoleMessage = function (msg) {
    console.log(msg);
}
page.open(args[1], function (status) {
    if (status === "success") {
        var eli = page.evaluate(function () {
            var els = document.getElementsByClassName("needsclick")[0];
            var ev = document.createEvent("MouseEvent");
            ev.initMouseEvent(
                "click",
                true /* bubble */, true /* cancelable */,
                window, null,
                0, 0, 0, 0, /* coordinates */
                false, false, false, false, /* modifier keys */
                0 /*left*/, null
            );
            els.dispatchEvent(ev);
            return 1;
        });
        phantom.waitFor(function () {
            return page.evaluate(function () {
                console.log(document.getElementsByClassName("fp-player").length);
                return document.getElementsByClassName("fp-player").length > 0;
            });
        });
        console.log(page.content);
    }
    else {
        console.log("ERROR");
    }
    phantom.exit();
});

function waitFor($config) {
    $config._start = $config._start || new Date();

    if ($config.timeout && new Date - $config._start > $config.timeout) {
        if ($config.error) $config.error();
        if ($config.debug) console.log('timedout ' + (new Date - $config._start) + 'ms');
        return;
    }

    if ($config.check()) {
        if ($config.debug) console.log('success ' + (new Date - $config._start) + 'ms');
        return $config.success();
    }

    setTimeout(waitFor, $config.interval || 0, $config);
}