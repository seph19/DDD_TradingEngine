DROP DATABASE IF EXISTS TradingEngine;
CREATE DATABASE TradingEngine;

USE TradingEngine
GO

IF NOT EXISTS(
	SELECT 1 FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[Currency]') 
		AND type IN (N'U') )
BEGIN
	CREATE TABLE [dbo].[Currency](
		CurrencyId INT IDENTITY(1,1) PRIMARY KEY,
		[Name] VARCHAR(25) NOT NULL,
		Ratio DECIMAL(18,2) NOT NULL
	)
END

IF NOT EXISTS(
	SELECT 1 FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[Balance]') 
		AND type IN (N'U') )
BEGIN
	CREATE TABLE [dbo].[Balance](
		BalanceId INT IDENTITY(1,1) PRIMARY KEY,
		CurrencyId INT NOT NULL,
		UserId INT NOT NULL,
		Amount DECIMAL(18,2) NOT NULL
	)
END

IF NOT EXISTS(
	SELECT 1 FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[User]') 
		AND type IN (N'U') )
BEGIN
	CREATE TABLE [dbo].[User](
		UserId INT IDENTITY(1,1) PRIMARY KEY,
		UserName VARCHAR(100) NOT NULL
	)
END


IF NOT EXISTS (SELECT 1 FROM dbo.Currency)
BEGIN
    INSERT INTO dbo.Currency
	(
		Name,
		Ratio
	)
	VALUES('USD',1),('EUR',0.92),('CAD',1.40),('AUD',1.57)
END


IF NOT EXISTS (SELECT 1 FROM dbo.[User])
BEGIN
	INSERT INTO dbo.[User]
	(
		UserName
	)
	VALUES ('Joe'),('Doe'),('John')
END