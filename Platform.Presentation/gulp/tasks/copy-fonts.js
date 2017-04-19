var gulp = require('gulp'),
    debug = require('gulp-debug'),
    paths = require('../config');

gulp.task('copy-fonts', function() {
    return gulp.src(paths.current.src.fonts)
        .pipe(gulp.dest(paths.current.dist.fonts));
});
