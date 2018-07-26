
drop database if exists marketapps;
create database marketapps;
use marketapps;
CREATE TABLE IF NOT exists `user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(80) NOT NULL,
  `phoneNumber` varchar(20) NOT NULL,
  `user_name` varchar(40) NOT NULL unique,
  `password` varchar(20) NOT NULL,
  PRIMARY KEY (`user_id`)
);


CREATE TABLE IF NOT exists `application` (
  `app_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(80) NOT NULL,
  `kind` varchar(100) NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `description` varchar(255) NOT NULL,
  `publisher` varchar(80) NOT NULL,
  `datepublish` datetime DEFAULT CURRENT_TIMESTAMP,
  `size` double NOT NULL,
  PRIMARY KEY (`app_id`)
);

CREATE TABLE IF NOT exists `payment` (
  `payment_id` int(11) NOT NULL auto_increment,
  `user_id` int(11) NOT NULL,
  `name` varchar(30) NOT NULL,
  `money` decimal(10,0) DEFAULT '0',
  PRIMARY KEY (`payment_id`,`user_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `payment_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
);

CREATE TABLE IF NOT exists `bill` (
  `bill_id` int(11) NOT NULL AUTO_INCREMENT,
  `app_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `payment_id` int(11) NOT NULL,
  `unitprice` decimal(10,0) DEFAULT NULL,
  `datacreate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`bill_id`),
  KEY `app_id` (`app_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `bill_ibfk_1` FOREIGN KEY (`app_id`) REFERENCES `application` (`app_id`),
  CONSTRAINT `bill_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`),
  CONSTRAINT `bill_ibfk_3` FOREIGN KEY (`payment_id`) REFERENCES `payment` (`payment_id`)
);


CREATE TABLE IF NOT exists `appbougth` (
  `user_id` int(11) NOT NULL,
  `app_id` int(11) NOT NULL,
  PRIMARY KEY (`user_id`,`app_id`),
  KEY `app_id` (`app_id`),
  CONSTRAINT `appbougth_ibfk_1` FOREIGN KEY (`app_id`) REFERENCES `application` (`app_id`),
  CONSTRAINT `appbougth_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
);

-- trigger --
DELIMITER $$
CREATE TRIGGER `tg_checkAppbought` BEFORE INSERT ON `bill` FOR EACH ROW begin
	if exists (select app_id from appbougth where user_id = new.user_id and app_id = new.app_id) then
		signal sqlstate '11111' set message_text = 'Wrong! this app has bougth!';
	end if;
end $$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER `tg_autoInsertAppbought` AFTER INSERT ON `bill` FOR EACH ROW begin
	insert into appbougth values(new.user_id, new.app_id);
end $$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER `tg_autoDeleteAppbougth` AFTER DELETE ON `bill` FOR EACH ROW begin
	delete from appbougth where app_id = old.app_id and user_id = old.user_id;
end $$
DELIMITER ;



-- insert data ---
INSERT INTO `user` VALUES (1,'Trần ngọc khánh','0964303957','khanhdrag9','123456'),(2,'Hà Văn Tùng','012345689','tungf05','123456');
INSERT INTO `application` VALUES (1,'flapy bird','game',20000.00,'là game vòng lặp','nguyễn hà đông','2018-07-18 21:39:52',5),(2,'flapy fist','game',10000.00,'phát triển từ tựa game nổi tiếng flapy bird','trần ngọc khánh','2018-07-18 21:39:52',5),(3,'Dota2','game',1000.00,'Là dòng game moba đình đám, kế thừa từ warcraft','War','2018-07-18 22:00:36',7000),(4,'LOL','Game Moba',11000.00,'game  teamwork','Riot','2018-07-20 10:28:39',7000),(5,'AOE','Game Chiến thuật',250000.00,'Game chiến thuật kinh điển','Microsoft','2018-07-20 10:28:39',100000),(6,'Call Of Duty','Gam FPS',200000.00,'Là Game Bắn sung chiến tranh','Values','2018-07-20 10:35:19',200000),(7,'Paladins','Game chiến thuật ',11000.00,'game teamwork','HiRez Studios','2018-07-20 20:54:51',4000),(8,'CrossFire','Game bắn súng',11000.00,'Game bắn súng đa thể loại','SmileGate','2018-07-20 20:54:51',6000),(9,'PUBG','Game sinh tồn',350000.00,'Game bắn súng chiến thuật','PUBG Corporation','2018-07-20 20:54:51',9000),(10,'MOTHERGUNSHIP','Game bắn súng',200000.00,'Game bắn súng viễn tưởng','Digital Grip','2018-07-20 20:54:51',7000),(11,'Gladius Relics of war','Game chiến thuật',300000.00,'Game chiến thuật chiến tranh','Slitherine Ltd','2018-07-20 20:54:51',8000),(12,'Dig or Die ','Game phiêu lưu',140000.00,'Game chiến thuật giải cứu','Game Gaddy','2018-07-20 20:54:51',3000),(13,'Mario','Game phiêu lưu',11000.00,'Game giải cứu','Nintendo','2018-07-20 20:54:51',500),(27,'Truy kích','Game bắn súng góc nhìn thứ nhất',11000.00,'Là game bắn súng đa thể loại nền tảng web','Vtc','2018-07-20 20:58:27',0);
INSERT INTO `payment` VALUES (1,1,'By Store',10000000),(2,2,'Visa',0),(3,1,'VietinBank',0);
INSERT INTO `bill`(app_id, user_id, payment_id, unitprice) values (2, 1, 1, 10000.00);

create user 'khanh'@'%' identified by '1234';
create user 'tung'@'%' identified by '1234';
create user 'khanh'@'localhost' identified by '1234';
create user 'tung'@'localhost' identified by '1234';
