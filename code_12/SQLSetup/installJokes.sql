if (charindex('4.2', @@version) > 0 or
    charindex('6.00', @@version) > 0 or
    charindex('6.50', @@version) > 0 )
   create database jokes on default = 3
else
   create database jokes
go

use jokes
go

/* object:  table [dbo].[users]  */
if exists (select * from sysobjects where id = object_id(
  N'[dbo].[users]') and objectproperty(id, N'isusertable') = 1)
drop table [dbo].[users]
go

/* object:  table [dbo].[jokes] */ 
if exists (select * from sysobjects where id = object_id(
  N'[dbo].[jokes]') and objectproperty(id, N'isusertable') = 1)
drop table [dbo].[jokes]
go

/* object:  table [dbo].[ratings] */ 
if exists (select * from sysobjects where id = object_id(
  N'[dbo].[ratings]') and objectproperty(id, N'isusertable') = 1)
drop table [dbo].[ratings]
go

/* object:  table [dbo].[users] */   
create table [dbo].[users] (
  [userName] [nvarchar] (20) not null primary key,
  [password] [nvarchar] (20) not null ,
  [isModerator] [bit] not null
) on [primary]
go

/* object:  table [dbo].[jokes] */
create table [dbo].[jokes] (
  [jokeID] [int] identity(1,1) primary key ,
  [joke] [nvarchar] (3500) not null ,
  [userName] [nvarchar] (20) not null ,
  [isModerated] [bit] not null ,
) on [primary]
go

/* object:  table [dbo].[ratings] */
create table [dbo].[ratings] (
  [jokeID] [int] not null references jokes(jokeID),
  [rating] [tinyint] not null,
) on [primary]
go

create  index "jokeID" on [dbo].[ratings](jokeID)
go

/* insert data into users table */
insert into users (userName,password, isModerator) values 
  ("admin","password", 1)

/* Create stored procedures */
if exists (select * from sysobjects where name = 'sp_manageUser' 
  and type = 'P')
  drop procedure sp_manageUser
go

if exists (select * from sysobjects where name = 'sp_checkUser' 
  and type = 'P')
  drop procedure sp_checkUser
go

if exists (select * from sysobjects where name = 'sp_manageJoke' 
  and type = 'P')
  drop procedure sp_manageJoke
go

if exists (select * from sysobjects where name = 'sp_manageRating' 
  and type = 'P')
  drop procedure sp_manageRating
go

if exists (select * from sysobjects where name = 'sp_returnJokes' 
  and type = 'P')
  drop procedure sp_returnJokes
go

create procedure sp_manageUser (
  -- add, modify, or delete a user
  @@userName nvarchar(20),
  @@password nvarchar(20),
  @@isModerator bit,
  @@action nvarchar(20),     -- one of 'add' or 'modify' or 'delete'
    -- returns:
    --  'S_OK'                 : success
    --  'F_userExists'      : failed: user already exists
    --  'F_userDoesNotExist': failed: user does not exist
    --  'F_unknownAction'   : action command unrecognized
  @@return nvarchar(20) output
      ) as

  declare @@userCount int 
  select @@userCount = count(*) from users where userName = @@userName

  -- sanity checks  
  if (@@userCount = 0 and ((@@action = 'modify') or 
    (@@action = 'delete')))
    begin
      select @@return = 'F_userDoesNotExist'
      return
    end
    
  if @@userCount = 1 and @@action = 'add'
    begin
      select @@return = 'F_userExists'
      return    
    end
    
  -- start
  if @@action = 'add'
    begin
      insert into users (userName,password,isModerator) 
        values (@@userName,@@password,@@isModerator)      
      select @@return = 'S_OK'    
      return
    end

  if @@action = 'delete'
    begin
      delete from users where userName = @@userName
      select @@return = 'S_OK'    
      return
    end 

  if @@action = 'modify'
    begin
      update users 
        set userName = @@userName,
        isModerator = @@isModerator
        where userName = @@userName
      if @@password is not null
        update users
          set password = @@password
          where userName = @@userName
      select @@return = 'S_OK'    
      return
    end 

  -- otherwise
  select @@return = 'F_unknownAction'
  return
go    

create procedure sp_checkUser (
  -- checks user information provided against information in 
  -- the database
  @@userName nvarchar(20),
  @@password nvarchar(20),
  @@isModerator bit,
    -- returns:
    --  'S_OK'            : information matches
    --  'F_userInfoWrong' : information does not match
  @@return nvarchar(20) output
      ) as

  declare @@userCount int 
  
  -- sanity checks
  if @@userName is null
    begin
      select @@return = 'F_userInfoWrong'
      return
    end
  
  -- start
  if @@password is null and @@isModerator is null
    begin
      select @@userCount = count(*) from users where 
        userName = @@userName
      goto checkCount
    end
    
  if @@isModerator is null
    begin
      select @@userCount = count(*) from users where 
        userName = @@userName and password = @@password
      goto checkCount
    end
    
  if @@password is null
    begin
      select @@userCount = count(*) from users where 
        userName = @@userName and isModerator = @@isModerator
      goto checkCount
    end
    
  select @@userCount = count(*) from users where userName = @@userName 
    and password = @@password and isModerator = @@isModerator

  checkCount:
  if @@userCount = 0
    begin
      select @@return = 'F_userInfoWrong'
      return
    end
    
  select @@return = 'S_OK'
  return

go    

create procedure sp_manageRating (
  -- add a joke rating
  @@jokeID int,
  @@rating tinyint,
  @@action nvarchar(20),     -- one of 'add' or 'delete'
    -- returns:
    --  'S_OK'              : success
    --  'F_jokeDoesNotExist': failed: joke does not exist
    --  'F_unknownAction'   : action command unrecognized
  @@return nvarchar(20) output
      ) as

  -- sanity checks on arguments done in middle tier

  declare @@jokeCount int 
  
  -- does the joke even exist?
  select @@jokeCount = count(*) from jokes where jokeID = @@jokeID
  if @@jokeCount = 0
    begin
      select @@return = 'F_jokeDoesNotExist'
      return    
    end

  if @@action = 'add'    
    begin
      insert into ratings (jokeID,rating) values (@@jokeID,@@rating)
      select @@return = 'S_OK'    
      return
    end
    
  if @@action = 'delete'
    begin
      delete from ratings where jokeID = @@jokeID
      select @@return = 'S_OK'    
      return
    end

  -- otherwise
  select @@return = 'F_unknownAction'
  return
go    

create procedure sp_manageJoke (
  -- add, modify, or delete a joke
  @@userName nvarchar(20),
  @@joke nvarchar(3500),
  @@isModerated bit,
  @@jokeID int,
  @@action nvarchar(20),     -- one of 'add' or 'modify' or 'delete'
    -- returns:
    --  'S_OK'              : success
    --  'F_jokeDoesNotExist': failed: joke does not exist
    --  'F_unknownAction'   : action command unrecognized
  @@return nvarchar(20) output
      ) as

  -- sanity checks on arguments done in middle tier

  declare @@jokeCount int 

  if @@action = 'add'    
    begin
      insert into jokes (userName,joke,isModerated) 
        values (@@userName,@@joke,@@isModerated)      
      select @@return = 'S_OK'    
      return
    end
    
  if @@action = 'modify'    
    begin
      select @@jokeCount = count(*) from jokes where jokeID = @@jokeID
      if @@jokeCount = 0
        begin
          select @@return = 'F_jokeDoesNotExist'
          return    
        end
      if @@isModerated is not null
        update jokes
          set isModerated = @@isModerated
          where jokeID = @@jokeID
      if @@userName is not null
        update jokes
          set userName = @@userName
          where jokeID = @@jokeID
      if @@joke is not null
        update jokes
          set joke = @@joke
          where jokeID = @@jokeID
      select @@return = 'S_OK'    
      return
    end

  if @@action = 'delete'
    begin
      select @@jokeCount = count(*) from jokes where jokeID = @@jokeID
      if @@jokeCount = 0
        begin
          select @@return = 'F_jokeDoesNotExist'
          return    
        end
      declare @@dummy nvarchar(40)        
      execute sp_manageRating @@jokeID, null, 'delete', @@dummy output
      delete from jokes where jokeID = @@jokeID
      select @@return = 'S_OK'    
      return
    end

  -- otherwise
  select @@return = 'F_unknownAction'
  return
go    

create procedure sp_returnJokes (
  -- returns jokes
  @@howMany int,
  @@isModerated bit,
  @@returnRandom bit
    -- returns a recordset containing jokeID, joke, and average rating
      ) as

  -- sanity checks on arguments done in middle tier

  declare @@jokeCount int 
  declare @baseJokeID int
  declare @baseJokeRelPos int
  declare @cmd varchar(1000)
  
  -- random start position?
  -- note that in this case, we implicitly assume that 
  --   * isModerated = 1
  --   * howMany <> null
  if @@returnRandom = 1
    begin
      select @@jokeCount = count(*) from jokes where isModerated = 1
      if @@jokeCount = 0
        return    
      
      if @@jokeCount < @@howMany
        set @@howMany = @@jokeCount

      -- get a random number between 0 and 1
      declare @random decimal(6,3)
      set @random = cast(datepart(ms, getdate()) as decimal(6,3))/1000
        
      -- set a random start position  
      set @baseJokeRelPos = 
        ((@@jokeCount - @@howMany + 1) * @random) + 1

      -- get the corresponding jokeID
      declare jokeTempCursor cursor scroll for select jokeID from 
        jokes where isModerated = 1 order by jokeID
      open jokeTempCursor
      fetch absolute @baseJokeRelPos from jokeTempCursor 
        into @baseJokeID
      close jokeTempCursor
      deallocate jokeTempCursor
    end
  
  -- start building our command
  set @cmd = 'select '
  
  if @@howMany is not null
    set @cmd = @cmd + 'top ' + cast(@@howMany as varchar(10)) + ' '
    
  set @cmd = @cmd + 'jokes.jokeID, left(ltrim(joke),3500) '  
  set @cmd = @cmd + ', cast(avg(cast(rating as decimal(5,4))) 
    as decimal(2,1)) '
  set @cmd = @cmd + 'from jokes left outer join ratings on 
    jokes.jokeID = ratings.jokeID '
  
  if @@isModerated is not null
    begin      
      if @@isModerated = 1
        begin
          set @cmd = @cmd + 'where isModerated = 1 '
          if @@returnRandom = 1
            set @cmd = @cmd + 'and jokes.jokeID >= ' + 
              cast(@baseJokeID as varchar(10)) + ' '
        end  
      if @@isModerated = 0
        set @cmd = @cmd + 'where isModerated = 0 '
    end
  
  set @cmd = @cmd + 'group by jokes.jokeID, joke order by jokes.jokeID'
  
  exec (@cmd)
go    

