var gulp = require('gulp'),
    debug = require('gulp-debug'),
    paths = require('../config'),
    connect = require('gulp-connect');

gulp.task('connectDev', function() {
    connect.server({
        root: paths.client.dist.index,
        port: 5025,
        livereload: true,
        fallback: paths.client.dist.index + '/index.html'
    });
});
