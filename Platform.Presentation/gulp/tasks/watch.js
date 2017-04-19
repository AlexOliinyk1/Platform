var gulp = require('gulp'),
    paths = require('../config');

gulp.task('watch', function() {
    gulp.watch(paths.current.src.templates, gulp.task('js-min-concat'));
    gulp.watch(paths.current.src.styles, gulp.task('css-concat'));
    gulp.watch(paths.current.src.app + '**/*.js', gulp.task('js-min-concat'));
    gulp.watch(paths.current.src.app + '/**/*.json', gulp.task('js-min-concat'));
});
