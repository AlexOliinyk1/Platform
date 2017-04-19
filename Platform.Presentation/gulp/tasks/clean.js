var del = require('del'),
    gulp = require('gulp'),
    paths = require('../config'),
    FwdRef = require('undertaker-forward-reference');

gulp.registry(FwdRef());

gulp.task('clean', function (cb) {
    return del([
        paths.current.public.app,
        paths.current.public.assets
    ], cb);
});
