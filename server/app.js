var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');

var indexRouter = require('./routes/index');
var dropRouter = require('./routes/drop');
var usersRouter = require('./routes/users');
var loginRouter = require('./routes/login');
var statusRouter = require('./routes/status');
var inventoryRouter = require('./routes/inventoryRoutes');

var app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', indexRouter);
app.use('/api/users', usersRouter);
app.use('/api/login', loginRouter);
app.use('/api/inventory', inventoryRouter);
app.use('/api/drop', dropRouter);
app.use('/api/status', statusRouter);

module.exports = app;
