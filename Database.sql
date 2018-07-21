

create user 'khanh'@'localhost' identified by '1234';
grant all on storeapp.* to 'khanh'@'%';

create user 'tung1'@'localhost' identified by '1234';
grant all on marketapps.* to 'tung'@'%';


drop database if exists marketapps;
create database marketapps;
use marketapps;
create table if not exists User(
		user_id int(11) auto_increment not null primary key,
        name varchar(80) not null,
        phoneNumber varchar(20) not null,
        user_name varchar(40) not null,
        password varchar(20) not null
);


create table if not exists application(
		app_id int(11) primary key auto_increment,
        name varchar(80) not null,
        kind varchar(30) not null,
        price decimal(10,2) not null,
        description varchar(255) not null,
        publisher varchar(80) not null,
        datepublish datetime default current_timestamp(),
        size double not null
);

create table if not exists bill(
bill_id int primary key auto_increment,
app_id int not null,
user_id int not null,
unitprice decimal(10,2) not null,
datacreate datetime default current_timestamp(),
foreign key (app_id) references application(app_id),
foreign key (user_id) references User(user_id)
);

create table if not exists payment(
payment_id int(11) not null,
user_id int(11) not null,
name varchar(20) not null,
foreign key (user_id) references User(user_id),
constraint pk_payment primary key(payment_id,user_id)
);


create table if not exists appbougth(
user_id int(11) not null,
app_id int(11) not  null,
constraint pk_appbougth primary key(user_id,app_id),
foreign key (app_id) references application(app_id),
foreign key (user_id) references User(user_id)
);