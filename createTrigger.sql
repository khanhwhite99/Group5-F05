select * from application;
select * from user;
select * from bill;
select * from appbougth;

insert into appbougth(user_id, app_id) values
(1, 2),(1,1);

insert into bill ( app_id, user_id, unitprice) values(12, 1, 20000);

delete from bill where bill_id = 34;
delete from appbougth where app_id = 6 and user_id = 2;


delimiter $$
create trigger tg_checkAppbought before insert on bill for each row
begin
	if exists (select app_id from appbougth where user_id = new.user_id and app_id = new.app_id) then
		signal sqlstate '11111' set message_text = 'Wrong! this app has bougth!';
	end if;
end$$
delimiter ;

delimiter $$
create trigger tg_autoInsertAppbought after insert on bill for each row
begin
	insert into appbougth values(new.user_id, new.app_id);
end$$
delimiter ;

delimiter $$
create trigger tg_autoDeleteAppbougth after delete on bill for each row
begin
	delete from appbougth where app_id = old.app_id and user_id = old.user_id;
end $$
delimiter ;

drop trigger tg_checkAppbought;
drop trigger tg_autoInsertAppbought;
drop trigger tg_autoDeleteAppbougth;