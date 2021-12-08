/// <binding BeforeBuild='compile:sass' ProjectOpened='watch:sass' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    cleanCss = require("gulp-clean-css"),
    gulpSass = require("gulp-sass")(require('sass'));

var webroot = "./wwwroot/";

gulp.task("compile:sass", function () {
    return gulp.src("./wwwroot/css/bootstrap.custom.scss")
        .pipe(gulpSass.sync())
        .pipe(cleanCss())
        .pipe(gulp.dest(webroot + 'css'));
});

gulp.task("minify:css", () => {
    return gulp.src('./wwwroot/css/bootstrap.custom.css')
        .pipe(cleanCss())
        .pipe(gulp.dest(webroot + 'css'));
});

gulp.task('watch:sass', function () {
    gulp.watch('./wwwroot/css/*.scss', (done) => {
        gulp.series(['compile:sass'])(done);
    });
});

gulp.task('clean:css', (callback) => {
    rimraf('./wwwroot/css/*.custom.css', callback);
});

gulp.task('build:js', () => {
    return gulp.src('node_modules/bootstrap/dist/js/bootstrap.bundle.min.js')
        .pipe(gulp.dest(webroot + 'js/'));
});
