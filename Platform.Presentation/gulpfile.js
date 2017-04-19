var del = require('del'),
    gulp = require('gulp'),
    paths = require('./gulp/config'),
    tasks = require('require-dir')('./gulp/tasks');

gulp.task('production', gulp.series(
    'html',
    'copy-fonts',
    'js-min-concat',
    'css-concat',
    'copy-images'
));

gulp.task('development', gulp.series(
    'html',
    'copy-fonts',
    'copy-source-maps',
    'js-min-concat',
    'css-concat',
    'copy-images',
    gulp.parallel('connectDev', 'watch')
));

gulp.task('build', done => {
    process.env.NODE_ENV = 'production';
    paths.current = paths.client;
    paths.project = 'client';
    return gulp.series('clean', 'production')(done);
});

gulp.task('default', done => {
    process.env.NODE_ENV = 'development';
    paths.current = paths.client;
    paths.project = 'client';
    return gulp.series('clean', 'development')(done);
});
