
-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'Users'
-- 
-- ---

DROP TABLE IF EXISTS `Users`;
		
CREATE TABLE `Users` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `EmailAddress` VARCHAR NULL DEFAULT NULL,
  `Password` VARCHAR NULL DEFAULT NULL,
  `FirstName` VARCHAR NULL DEFAULT NULL,
  `LastName` VARCHAR NULL DEFAULT NULL,
  `PhoneNumber` TINYINT NULL DEFAULT NULL,
  `Created` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'UserProfiles'
-- 
-- ---

DROP TABLE IF EXISTS `UserProfiles`;
		
CREATE TABLE `UserProfiles` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `UserId` TINYINT NULL DEFAULT NULL,
  `IPAddress` CHAR NULL DEFAULT NULL,
  `LoginAttempts` TINYINT NULL DEFAULT NULL,
  `IsVerified` TINYINT NULL DEFAULT NULL,
  `IsLocked` TINYINT NULL DEFAULT NULL,
  `IsLoggedIn` TINYINT NULL DEFAULT NULL,
  `LastModified` DATETIME NULL DEFAULT NULL,
  `LastLogin` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'Groups'
-- 
-- ---

DROP TABLE IF EXISTS `Groups`;
		
CREATE TABLE `Groups` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `GroupName` VARCHAR NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'Roles'
-- 
-- ---

DROP TABLE IF EXISTS `Roles`;
		
CREATE TABLE `Roles` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `RoleName` TINYINT NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'UserGroups'
-- 
-- ---

DROP TABLE IF EXISTS `UserGroups`;
		
CREATE TABLE `UserGroups` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `UserId` TINYINT NULL DEFAULT NULL,
  `GroupId` TINYINT NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'UserRoles'
-- 
-- ---

DROP TABLE IF EXISTS `UserRoles`;
		
CREATE TABLE `UserRoles` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `UserId` TINYINT NULL DEFAULT NULL,
  `RoleId` TINYINT NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'Applications'
-- 
-- ---

DROP TABLE IF EXISTS `Applications`;
		
CREATE TABLE `Applications` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `IsActive` TINYINT NULL DEFAULT NULL,
  `Name` VARCHAR NULL DEFAULT NULL,
  `Key` VARCHAR NULL DEFAULT NULL,
  `Added` DATETIME NULL DEFAULT NULL,
  `Modified` DATETIME NULL DEFAULT NULL,
  `DefaultTimeoutLength` TINYINT NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'UserApplications'
-- 
-- ---

DROP TABLE IF EXISTS `UserApplications`;
		
CREATE TABLE `UserApplications` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `UserId` TINYINT NULL DEFAULT NULL,
  `ApplicationId` TINYINT NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'UserActivities'
-- 
-- ---

DROP TABLE IF EXISTS `UserActivities`;
		
CREATE TABLE `UserActivities` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `UserId` TINYINT NULL DEFAULT NULL,
  `ApplicationId` TINYINT NULL DEFAULT NULL,
  `ActivityTypeId` TINYINT NULL DEFAULT NULL,
  `LoginTime` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'ActivityTypes'
-- 
-- ---

DROP TABLE IF EXISTS `ActivityTypes`;
		
CREATE TABLE `ActivityTypes` (
  `id` TINYINT NULL AUTO_INCREMENT DEFAULT NULL,
  `Description` VARCHAR NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Foreign Keys 
-- ---

ALTER TABLE `UserProfiles` ADD FOREIGN KEY (UserId) REFERENCES `Users` (`id`);
ALTER TABLE `UserGroups` ADD FOREIGN KEY (UserId) REFERENCES `Users` (`id`);
ALTER TABLE `UserGroups` ADD FOREIGN KEY (GroupId) REFERENCES `Groups` (`id`);
ALTER TABLE `UserRoles` ADD FOREIGN KEY (UserId) REFERENCES `Users` (`id`);
ALTER TABLE `UserRoles` ADD FOREIGN KEY (RoleId) REFERENCES `Roles` (`id`);
ALTER TABLE `UserApplications` ADD FOREIGN KEY (UserId) REFERENCES `Users` (`id`);
ALTER TABLE `UserApplications` ADD FOREIGN KEY (ApplicationId) REFERENCES `Applications` (`id`);
ALTER TABLE `UserActivities` ADD FOREIGN KEY (UserId) REFERENCES `Users` (`id`);
ALTER TABLE `UserActivities` ADD FOREIGN KEY (ApplicationId) REFERENCES `Applications` (`id`);
ALTER TABLE `UserActivities` ADD FOREIGN KEY (ActivityTypeId) REFERENCES `ActivityTypes` (`id`);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `Users` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `UserProfiles` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `Groups` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `Roles` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `UserGroups` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `UserRoles` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `Applications` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `UserApplications` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `UserActivities` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `ActivityTypes` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `Users` (`id`,`EmailAddress`,`Password`,`FirstName`,`LastName`,`PhoneNumber`,`Created`) VALUES
-- ('','','','','','','');
-- INSERT INTO `UserProfiles` (`id`,`UserId`,`IPAddress`,`LoginAttempts`,`IsVerified`,`IsLocked`,`IsLoggedIn`,`LastModified`,`LastLogin`) VALUES
-- ('','','','','','','','','');
-- INSERT INTO `Groups` (`id`,`GroupName`) VALUES
-- ('','');
-- INSERT INTO `Roles` (`id`,`RoleName`) VALUES
-- ('','');
-- INSERT INTO `UserGroups` (`id`,`UserId`,`GroupId`) VALUES
-- ('','','');
-- INSERT INTO `UserRoles` (`id`,`UserId`,`RoleId`) VALUES
-- ('','','');
-- INSERT INTO `Applications` (`id`,`IsActive`,`Name`,`Key`,`Added`,`Modified`,`DefaultTimeoutLength`) VALUES
-- ('','','','','','','');
-- INSERT INTO `UserApplications` (`id`,`UserId`,`ApplicationId`) VALUES
-- ('','','');
-- INSERT INTO `UserActivities` (`id`,`UserId`,`ApplicationId`,`ActivityTypeId`,`LoginTime`) VALUES
-- ('','','','','');
-- INSERT INTO `ActivityTypes` (`id`,`Description`) VALUES
-- ('','');
