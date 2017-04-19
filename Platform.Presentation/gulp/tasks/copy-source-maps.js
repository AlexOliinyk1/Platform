var gulp = require('gulp'),
    debug = require('gulp-debug'),
    paths = require('../config'),
    flatten = require('gulp-flatten');

gulp.task('copy-source-maps', function() {
    return gulp.src(paths.current.src.bower + '/**/*.js.map')
        .pipe(flatten())
        .pipe(gulp.dest(paths.current.dist.js));
});
