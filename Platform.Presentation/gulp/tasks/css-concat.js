var nib = require('nib'),
    env = process.env.NODE_ENV,
    gulp = require('gulp'),
    pump = require('pump'),
    gutil = require('gulp-util'),
    debug = require('gulp-debug'),
    paths = require('../config'),
    stylus = require('gulp-stylus'),
    concat = require('gulp-concat'),
    FwdRef = require('undertaker-forward-reference'),
    connect = require('gulp-connect'),
    uglifycss = require('gulp-uglifycss'),
    orderedMergeStream = require('ordered-merge-stream');
    

gulp.registry(FwdRef());

gulp.task('css-concat', function() {
    var environment = env === 'production' ? uglifycss({"cuteComments": true}) : gutil.noop();

    var css = gulp.src(paths.global.src.css);

    return pump([
        orderedMergeStream([css]),
        concat('app.min.css'),
        environment,
        gulp.dest(paths.current.dist.css),
        connect.reload()
    ], function(err) {
        if (err) {
            console.log('pipe finished with error:', err);
        }
    });
});
