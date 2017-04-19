    var gulp = require('gulp'),
    debug = require('gulp-debug'),
    paths = require('../config'),
    connect = require('gulp-connect'),
    packageJSON = require('../../package.json');

gulp.task('html', function() {
    return gulp.src(paths.current.src.index)
        .pipe(gulp.dest(paths.current.dist.index))
        .pipe(connect.reload());
});
