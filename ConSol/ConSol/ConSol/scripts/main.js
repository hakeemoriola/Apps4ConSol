/* global Handlebars */
(function () {
    'use strict';
    window.App = {
        init: function (config) {
            console.log('1 - initialize');
            this.config = config;
            if (this.config.feature && this.config.feature.endpoint) {
                this.fetch();
            }
            return this;
        },
        render: function () {
            console.log('4 - render');
            var template = Handlebars.compile($(this.config.el).find(
            'script[type="text/x-handlebars-template"]').html());
            var html = template(this.config);
            $(this.config.el).html(html);
            return this;
        },
        onSuccess: function (response) {
            console.log('3 - onSuccess');
            this.config.features = response;
            this.render();
        },
        onError: function (error) {
            return this.log(error);
        },
        fetch: function () {
            console.log('2 - fetch');
            var self = this;
            $.ajax({
                url: self.config.feature.endpoint,
                dataType: 'jsonp',
                success: function (results) {
                    return self.onSuccess(results);
                },
                error: function (error) {
                    return self.onError(error);
                }
            });
        }
    };
})();

console.log('\'Allo \'Allo!'); // eslint-disable-line no-console