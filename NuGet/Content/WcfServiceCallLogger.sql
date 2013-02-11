

/****** Object:  Table [dbo].[LogTable]    Script Date: 09/02/2013 11:21:02 ******/
/*
DROP TABLE [dbo].[LogTable]
GO
*/

/****** Object:  Table [dbo].[LogTable]    Script Date: 09/02/2013 11:21:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
SET ANSI_PADDING ON
GO



CREATE TABLE [dbo].[LogTable](
	[timestamp] [datetime] NULL,
	[level] [varchar](50) NULL,
	[logger] [varchar](150) NULL,
	[requestid] [varchar](50) NULL,
	[service] [varchar](50) NULL,
	[action] [varchar](50) NULL,
	[invocation] [varchar](4000) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
*/

--total distinct calls
select action,count(distinct requestid) requests , count(*) total
from [dbo].[LogTable]
group by action


--is it lookup stuff? could cache the whole service
select servicename, sum(total) from 
(
select case when (invocation like 'ILookupService%') then 'Lookup' else 'Service' end servicename, count(*) total
from [dbo].[LogTable]
group by invocation

) g
group by servicename

--multi requests per page - easy to fix these

select requestid, invocation, count(*) 
from [dbo].[LogTable]
group by requestid, invocation
order by count(*) desc


select  invocation, count(*) 
from [dbo].[LogTable]
--where invocation like 'IProd%'
group by  invocation
order by count(*) desc
