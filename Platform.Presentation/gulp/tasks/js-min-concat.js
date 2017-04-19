var env = process.env.NODE_ENV,
    gulp = require('gulp'),
    pump = require('pump'),
    gutil = require('gulp-util'),
    debug = require('gulp-debug'),
    merge = require('merge-stream'),
    concat = require('gulp-concat'),
    paths = require('../config'),
    FwdRef = require('undertaker-forward-reference'),
    flatten = require('gulp-flatten'),
    connect = require('gulp-connect'),
    compress = require('gulp-uglify'),
    orderedMergeStream = require('ordered-merge-stream'),
    templateCache = require('gulp-angular-templatecache'),
    babel = require('gulp-babel');


gulp.registry(FwdRef());

gulp.task('js-min-concat', gulp.series(function() {

    var environment = env === 'production' ? compress() : gutil.noop();
    var jsBefore = gulp.src(paths.global.src.jsBefore);
    var jsAfter = gulp.src(paths.global.src.jsAfter);

    var templates = gulp.src(paths.current.src.templates)
        .pipe(flatten())
        .pipe(templateCache({
            module: 'app.templates',
            root: '',
            standalone: true
        }));

    var js = gulp.src(paths.current.src.js)
        .pipe(environment);

    return pump([
        orderedMergeStream([jsBefore, jsAfter, js]),
        concat('app.min.js'),
        gulp.dest(paths.current.dist.js),
        connect.reload()
    ], function(err) {
        if (err) {
            console.log('pipe finished with error:', err);
        }
    });
}));
