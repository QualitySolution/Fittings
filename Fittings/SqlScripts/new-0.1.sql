-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema Fittings
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Table `users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `users` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `login` VARCHAR(45) NOT NULL,
  `deactivated` TINYINT(1) NOT NULL,
  `email` VARCHAR(45) NULL DEFAULT NULL,
  `description` TEXT NULL DEFAULT NULL,
  `admin` TINYINT(1) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `base_parameters`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `base_parameters` (
  `name` VARCHAR(40) NOT NULL,
  `str_value` VARCHAR(200) NULL DEFAULT NULL,
  PRIMARY KEY (`name`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `diameter`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `diameter` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `inch` VARCHAR(5) NOT NULL,
  `mm` VARCHAR(5) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `pressure`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `pressure` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `PN` VARCHAR(10) NOT NULL,
  `Pclass` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `connection_type`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `connection_type` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name_rus` VARCHAR(200) NULL,
  `name_eng` VARCHAR(200) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `body_material`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `body_material` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name_rus` VARCHAR(200) NULL,
  `name_eng` VARCHAR(200) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `provider`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `provider` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(200) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitting_type`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitting_type` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name_rus` VARCHAR(100) NULL,
  `name_eng` VARCHAR(100) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fittings`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fittings` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `fitting_name_id` INT UNSIGNED NULL,
  `diametr_id` INT UNSIGNED NOT NULL,
  `diametr_units` ENUM('inch', 'mm') NULL,
  `pressure_id` INT UNSIGNED NOT NULL,
  `pressure_units` ENUM('PN', 'Pclass') NULL,
  `connection_type_id` INT UNSIGNED NOT NULL,
  `body_material_id` INT UNSIGNED NOT NULL,
  `code` VARCHAR(45) NOT NULL,
  `note` TEXT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_fittings_1_idx` (`fitting_name_id` ASC),
  INDEX `fk_fittings_2_idx` (`pressure_id` ASC),
  INDEX `fk_fittings_3_idx` (`diametr_id` ASC),
  INDEX `fk_fittings_5_idx` (`connection_type_id` ASC),
  INDEX `fk_fittings_6_idx` (`body_material_id` ASC),
  CONSTRAINT `fk_fittings_1`
    FOREIGN KEY (`fitting_name_id`)
    REFERENCES `fitting_type` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_fittings_2`
    FOREIGN KEY (`pressure_id`)
    REFERENCES `pressure` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_fittings_3`
    FOREIGN KEY (`diametr_id`)
    REFERENCES `diameter` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_fittings_5`
    FOREIGN KEY (`connection_type_id`)
    REFERENCES `connection_type` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_fittings_6`
    FOREIGN KEY (`body_material_id`)
    REFERENCES `body_material` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `files`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `files` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `file` LONGBLOB NOT NULL,
  `fitting_id` INT UNSIGNED NOT NULL,
  `file_name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_files_1_idx` (`fitting_id` ASC),
  CONSTRAINT `fk_files_1`
    FOREIGN KEY (`fitting_id`)
    REFERENCES `fittings` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `conductor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `conductor` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name_rus` VARCHAR(200) NULL,
  `name_eng` VARCHAR(200) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `price`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `price` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `price_date` DATE NOT NULL,
  `provider_id` INT UNSIGNED NOT NULL,
  `comment` TEXT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_price_1_idx` (`provider_id` ASC),
  CONSTRAINT `fk_price_1`
    FOREIGN KEY (`provider_id`)
    REFERENCES `provider` (`id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `project`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `customer` VARCHAR(100) NULL,
  `project_name` VARCHAR(100) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `price_items`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `price_items` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `fitting_id` INT UNSIGNED NOT NULL,
  `price_id` INT UNSIGNED NOT NULL,
  `price_units` ENUM('USD', 'EUR', 'RUB') NOT NULL,
  `cost` DECIMAL(12,2) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_price_items_1_idx` (`fitting_id` ASC),
  INDEX `fk_price_items_2_idx` (`price_id` ASC),
  CONSTRAINT `fk_price_items_1`
    FOREIGN KEY (`fitting_id`)
    REFERENCES `fittings` (`id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_price_items_2`
    FOREIGN KEY (`price_id`)
    REFERENCES `price` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `project_items`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project_items` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `project_id` INT UNSIGNED NULL,
  `fitting_id` INT UNSIGNED NOT NULL,
  `sequence_number` INT NOT NULL,
  `TRP_position` VARCHAR(45) NULL,
  `amount` INT UNSIGNED NOT NULL DEFAULT 1,
  `conductor_id` INT UNSIGNED NULL,
  `row_group` VARCHAR(200) NULL,
  `location` VARCHAR(200) NULL,
  `temperature_min` INT(5) NULL,
  `temperature_max` INT(5) NULL,
  `comment` TEXT NULL,
  `price` DECIMAL(12,2) UNSIGNED NULL DEFAULT NULL,
  `currency` ENUM('USD', 'EUR', 'RUB') NULL DEFAULT 'USD',
  `price_item_id` INT UNSIGNED NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_project_1_idx` (`conductor_id` ASC),
  UNIQUE INDEX `index6` (`project_id` ASC, `sequence_number` ASC),
  INDEX `fk_project_items_3_idx` (`fitting_id` ASC),
  INDEX `fk_project_items_1_idx` (`price_item_id` ASC),
  CONSTRAINT `fk_project_1`
    FOREIGN KEY (`conductor_id`)
    REFERENCES `conductor` (`id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE,
  CONSTRAINT `fk_project_items_2`
    FOREIGN KEY (`project_id`)
    REFERENCES `project` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_project_items_3`
    FOREIGN KEY (`fitting_id`)
    REFERENCES `fittings` (`id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_project_items_1`
    FOREIGN KEY (`price_item_id`)
    REFERENCES `price_items` (`id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `base_parameters`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `base_parameters` (`name`, `str_value`) VALUES ('edition', 'com');
INSERT INTO `base_parameters` (`name`, `str_value`) VALUES ('product_name', 'Fittings');
INSERT INTO `base_parameters` (`name`, `str_value`) VALUES ('version', '0.1');

COMMIT;


-- -----------------------------------------------------
-- Data for table `diameter`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '3/8″', '10');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '1/2″', '15');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '3/4″', '20');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '1″', '25');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '1 1/4″', '32');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '1 1/2″', '40');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '2″', '50');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '2 1/2″', '65');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '3″', '80');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '4″', '100');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '5″', '125');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '6″', '150');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '7″', '175');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '8″', '200');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '10″', '250');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '12″', '300');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '14″', '350');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '16″', '400');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '18″', '450');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '20″', '500');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '24″', '600');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '28″', '700');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '32″', '800');
INSERT INTO `diameter` (`id`, `inch`, `mm`) VALUES (DEFAULT, '40″', '1000');

COMMIT;


-- -----------------------------------------------------
-- Data for table `pressure`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `pressure` (`id`, `PN`, `Pclass`) VALUES (DEFAULT, 'PN20', '150#');
INSERT INTO `pressure` (`id`, `PN`, `Pclass`) VALUES (DEFAULT, 'PN50', '300#');
INSERT INTO `pressure` (`id`, `PN`, `Pclass`) VALUES (DEFAULT, 'PN68', '400#');
INSERT INTO `pressure` (`id`, `PN`, `Pclass`) VALUES (DEFAULT, 'PN100', '600#');
INSERT INTO `pressure` (`id`, `PN`, `Pclass`) VALUES (DEFAULT, 'PN150', '900#');
INSERT INTO `pressure` (`id`, `PN`, `Pclass`) VALUES (DEFAULT, 'PN250', '1500#');
INSERT INTO `pressure` (`id`, `PN`, `Pclass`) VALUES (DEFAULT, 'PN420', '2500#');

COMMIT;

