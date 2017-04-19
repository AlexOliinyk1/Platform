var paths = {
    global: {
        src: {
            jsBefore: [
                './bower_components/jquery/dist/jquery.min.js',
                './bower_components/lodash/dist/lodash.min.js',
                './bower_components/angular/angular.min.js'
            ],
            jsAfter: [
                './bower_components/angular-animate/angular-animate.min.js',
                './bower_components/angular-ui-router/release/angular-ui-router.min.js',
                './bower_components/angular-resource/angular-resource.min.js',
                './bower_components/angular-smart-table/dist/smart-table.min.js',
                './bower_components/AngularJS-Toaster/toaster.min.js',
                './bower_components/bootstrap/dist/js/bootstrap.min.js'
            ],
            css: [
                './bower_components/bootstrap/dist/css/bootstrap.min.css',
                './bower_components/AngularJS-Toaster/toaster.min.css',
            ]
        }
    },
    client: {
        public: {
            app: 'dist/app/**/*',
            assets: 'dist/assets/**/*',
        },
        src: {
            app: './src/app/',
            index: ['./src/app/index.html'],
            templates: ['./src/app/components/**/*.html'],
            js: [
                './src/app/app.js'
            ],
            stylus: [
                './src/assets/styles/app.styl'
            ],
            styles: [
                './src/assets/styles/**/*.styl'
            ],
            images: [
                './src/assets/img/**/*.*'
            ],
            fonts: [
                './src/assets/fonts/**/*.*'
            ]
        },
        dist: {
            js: './dist/app/',
            css: './dist/assets/css',
            fonts: './dist/assets/fonts',
            images: './dist/assets/i',
            index: './dist'
        }
    }
};

module.exports = paths;