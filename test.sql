use marketapps;

alter table bill modify column unitprice decimal null;

update bill b set unitprice = (select price from Application p where p.app_id = b.app_id);
drop trigger tg_bill_unitprice;
delete from bill where bill_id = 17;

desc bill;
desc application;
desc user;


select * from bill;
select * from user;
select * from application;


insert into application(name,kind,price,description,publisher,size)
values
('LOL','Game Moba',0,'game hành teamwork','Riot',7000),
('AOE', 'Game Chiến thuật', 250000, 'Game chiến thuật kinh điển', 'Microsoft', 100000);

insert into user(name,phoneNumber,user_name,password)
values('khánh','0123456789','ông trùm khánh','khanhvuinhon');

insert into bill(app_id,user_id,unitprice)
values(11,11,11000);






select * from bill order by rand() limit 1;