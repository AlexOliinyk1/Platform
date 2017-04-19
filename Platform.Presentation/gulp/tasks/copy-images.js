var gulp = require('gulp'),
    debug = require('gulp-debug'),
    paths = require('../config');

gulp.task('copy-images', function() {
    return gulp.src(paths.current.src.images)
        .pipe(gulp.dest(paths.current.dist.images));
});
