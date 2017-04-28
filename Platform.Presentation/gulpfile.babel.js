import gulp from 'gulp';
import concat from 'gulp-concat';
import wrap from 'gulp-wrap';
import uglify from 'gulp-uglify';
import htmlmin from 'gulp-htmlmin';
import gulpif from 'gulp-if';
import sass from 'gulp-sass';
import yargs from 'yargs';
import ngAnnotate from 'gulp-ng-annotate';
import templateCache from 'gulp-angular-templatecache';
import server from 'browser-sync';
import del from 'del';
import path from 'path';

const argv = yargs.argv;
const root = 'src/';
const paths = {
  dist: './dist/',
  scripts: `${root}/app/**/*.js`,
  styles: `${root}/assets/sass/*.scss`,
  templates: `${root}/app/**/*.template.html`,
  modules: [
    'jquery/dist/jquery.js',
    'angular/angular.js',
    'angular-ui-router/release/angular-ui-router.js',
    'angular-animate/angular-animate.js',
    'angular-resource/angular-resource.js',
    'angular-loading-bar/build/loading-bar.js',
    'angular-smart-table/dist/smart-table.js',
    'AngularJS-Toaster/toaster.js',
    'bootstrap/dist/js/bootstrap.js'
  ],
  modulesCSS:[
    'angular-loading-bar/build/loading-bar.min.css',
    'AngularJS-Toaster/toaster.min.css',
    'bootstrap/dist/css/bootstrap.min.css',
  ],
  static: [
    `${root}/index.html`,
    `${root}/assets/fonts/**/*`,
    `${root}/assets/img/**/*`
  ]
};

server.create();

gulp.task('clean', cb => del(paths.dist + '**/*', cb));

gulp.task('templates', () => {
  return gulp.src(paths.templates)
    .pipe(htmlmin({ collapseWhitespace: true }))
    .pipe(templateCache({
      root: 'app',
      standalone: true,
      transformUrl: function (url) {
        return url.replace(path.dirname(url), '.');
      }
    }))
    .pipe(gulp.dest('./'));
});

gulp.task('modules', ['templates','modulesCSS'], () => {
  return gulp.src(paths.modules.map(item => 'bower_components/' + item))
    .pipe(concat('vendor.js'))
    .pipe(gulpif(argv.deploy, uglify()))
    .pipe(gulp.dest(paths.dist + 'js/'));
});

gulp.task('modulesCSS', () => {
  return gulp.src(paths.modulesCSS.map(item => 'bower_components/' + item))
    .pipe(concat('vendor.css'))
    .pipe(gulpif(argv.deploy, uglify()))
    .pipe(gulp.dest(paths.dist + 'css/'));
});

gulp.task('styles',['copy'], () => {
  return gulp.src(paths.styles)
    .pipe(sass({outputStyle: 'compressed'}))
    .pipe(gulp.dest(paths.dist + 'css/'));
});

gulp.task('scripts', ['modules'], () => {
  return gulp.src([
    `${root}/app/**/*.module.js`,
    paths.scripts,
    './templates.js'
  ])
    .pipe(wrap('(function(angular){\n\'use strict\';\n<%= contents %>})(window.angular);'))
    .pipe(concat('bundle.js'))
    .pipe(ngAnnotate())
    .pipe(gulpif(argv.deploy, uglify()))
    .pipe(gulp.dest(paths.dist + 'js/'));
});

gulp.task('serve', () => {
  return server.init({
    files: [`${paths.dist}/**`],
    port: 5000,
    server: {
      baseDir: paths.dist
    }
  });
});

gulp.task('copy', ['clean'], () => {
  return gulp.src(paths.static, { base: 'src' })
    .pipe(gulp.dest(paths.dist));
});

gulp.task('watch', ['serve', 'scripts'], () => {
  gulp.watch([paths.scripts, paths.templates], ['scripts']);
  gulp.watch(paths.styles, ['styles']);
});

gulp.task('default', [
  'styles',
  'serve',
  'watch'
]);

gulp.task('production', [
  'copy',
  'scripts'
]);