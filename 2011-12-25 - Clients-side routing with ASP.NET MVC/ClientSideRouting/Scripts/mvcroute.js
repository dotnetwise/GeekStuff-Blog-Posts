var Url = (function () {

    var routes = [];

    var buildRoute = function (name, params) {
        var route = routes[name];

        if (params == undefined)
            params = [];

        if (route == undefined) {
            alert('Could not find any route with name ' + name + '.');
            return '';
        }

        if (route.defaults != undefined) {
            for (var key in route.defaults) {
                var paramValue = (params[key] == undefined) ? undefined : params[key];
                var defaultValue = (route.defaults[key] == undefined) ? undefined : route.defaults[key];

                if (paramValue == undefined && defaultValue != undefined) {
                    params[key] = route.defaults[key];
                }
            }
        }
        return route.func(params);
    };

    var addRoute = function (name, urlBuilderFunction, defaults) {
        routes[name] = { 
            func: urlBuilderFunction, defaults: defaults 
        };
    }

    return {
        Route: buildRoute,
        AddRoute: addRoute
    };

})();