/// <binding BeforeBuild='process:sass' ProjectOpened='watch' />
"use strict";
const gulp = require("gulp"),
    sass = require("gulp-sass"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    plumber = require('gulp-plumber'),
    autoprefixer = require('autoprefixer'),
    postcss = require('gulp-postcss'),
    notify = require('gulp-notify');
const paths = {
    webroot: "./Content/",
    bower: "./bower_components/",
    lib: "./Content/lib/",
    jsDest: "./Scripts/"
};
paths.cssDest = paths.webroot + "css";
paths.appJsDest = "./Scripts/app/";
paths.appBaseJs = paths.appJsDest + "base/**/*.js";
paths.appServicesJs = paths.appJsDest + "services/**/*.js";
paths.appComponentsJs = paths.appJsDest + "components/**/*.js";
paths.datatablesResponsiveCss = paths.bower + "datatables-responsive/css/";
gulp.task("deploy", ["process:sass"]);
gulp.task("process:sass", () => {
    return gulp.src('Styles/site.scss')
        .pipe(plumber({
            errorHandler: function (err) {
                notify.onError({
                    title: "Gulp error in " + err.plugin,
                    message: err.toString()
                })(err);
            }
        }))
        .pipe(sass())
        .pipe(concat('site.css'))
        .pipe(postcss([autoprefixer()]))
        .pipe(gulp.dest(paths.cssDest))
        .pipe(concat('site.min.css'))
        .pipe(postcss([autoprefixer()]))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.cssDest));
});

gulp.task("watch", () => {
    gulp.watch('Styles/**/*.scss', ["process:sass"]);
});
