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
  `inch` VARCHAR(10) NOT NULL,
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


-- -----------------------------------------------------
-- Data for table `connection_type`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, ' штуцерное', 'nippe');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'быстроразъемное', 'quick-coupling');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'быстроразъемное, резьбовое', 'quick-coupling, threaded');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'другое', 'other');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'межфланцевое ', 'lug type');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'межфланцевое', 'wafer-type');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'муфтовое', 'female');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'под сварку', 'for welding');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'резьбовое', 'threaded');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'фланцевое ', 'flanged');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'фланцевое - быстросмыкающегося типа', 'flanged - quick closing type');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'фланцевое или муфтовое', 'flanged or nippel');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'фланцевое или штуцерное', 'flanged or nippel');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'штуцерное', 'nipple joint');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'штуцерное', 'screwed');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'штуцерное, фланцевое', 'nipple joint, flanged');
INSERT INTO `connection_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'штуцерное, фланцевое', 'screwed, flanged');

COMMIT;


-- -----------------------------------------------------
-- Data for table `body_material`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `body_material` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, ' кованная сталь', ' forged steel');
INSERT INTO `body_material` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'нержавеющая сталь ', 'stainless steel');
INSERT INTO `body_material` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, ' сталь', 'steel');
INSERT INTO `body_material` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'алюм бронза', 'AL bronze');
INSERT INTO `body_material` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'бронза', 'bronze');
INSERT INTO `body_material` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'корпус из нержавеющей стали с резиновой внутренней облицовкой', 'Body - stainless steel with rubber lining');
INSERT INTO `body_material` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'латунь', 'brass');
INSERT INTO `body_material` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'серый чугун', 'cast iron');
INSERT INTO `body_material` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'чугун с шаровидным графитом', 'nodular cast iron');

COMMIT;


-- -----------------------------------------------------
-- Data for table `fitting_type`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Автоматический воздухоотводчик поплавковый', 'Automatic air trap');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Автоматический дыхательный клапан', 'Automatic air valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Вакуумный клапан', 'Vacuum valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Влагомаслоотделитель', 'Oil/water separatop');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Воздухоотводчик', 'Air vent valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Вставка амортизационная', 'Compensator joint');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Задвижка клинкетная', 'Gate valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Затвор поворотный дисковый', 'Butterfly valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Затвор поворотный дисковый (тип LUG)', 'Butterfly valve (LUG type)');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Захлопка', 'Swing check valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Захлопка без средств принудительного закрытия [двойной обратный клапан]', 'non-return flap without positive means of closing [dual check valve]');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Захлопка невозвратная межфланцевая', 'Non-return flap valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Захлопка невозвратная с принудительным закрытием', 'Non-return flap valve with forced closing');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан вакуумный антисифонный', 'vacuum breaker air valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан запорный', 'Stop valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан запорный (для выпуска воздуха)', 'Stop valve (for air discharge)');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан запорный МА1414С ф.BESI', 'Shut-off valve MA1414S f.BESI');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан запорный с электромагнитным приводом', 'Stop valve with electromagnetic drive');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан запорный угловой', 'Stop angle valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан невозвратно-запорный', 'Stop-check valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан невозвратно-запорный', 'SDNR valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан невозвратно-запорный угловой', 'Angle stop-check valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан невозвратно-приемный с сеткой', 'inlet non-return valve [foot valve] with strainer');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан невозвратный', 'Non-return valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан пожарный с головкой соединительной и заглушкой', 'Fire valve with connection head and plug');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан пожарный с стандартной соединительной головкой быстросмыкающегося типа и заглушкой', 'Fire valve with standart connection head and plug');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан пожарный угловой', 'Angle fire valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан предохранительный', 'Safety valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан предохранительный угловой', 'Angle safety valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан регулирующий', 'Control valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан редукционный', 'reducing valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан редукционный с манометром', 'Reducing valve with pressure gauge');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан самозапорный', 'Self-closing valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан самозапорный для измерительных труб', 'Sounding pipe self-closing valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан спускной поплавковый', 'Liquid drainer');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан терморегулирующий', 'Thermostatic control valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан терморегулирующий (электрический)', 'Thermostatic control valve (electric)');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Клапан шаровой', 'ball valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Конденсатоотводчик автоматический', 'Automatic steam trap');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Коробка грязевая', 'mud box');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Кран водопробный', 'Water-gauge valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Кран запорный спускной', 'Drain valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Кран проходной', 'Straight cock');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Кран расходный', 'Tap cock');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Кран трехходовой с Т-образной пробкой', 'Three-way cock, T-port');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Кран трехходовой с L-образной пробкой', '2/3-way cock');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Международное береговое соединение Ду65 в комплекте', 'International shore connection DN65');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Переносной шланг осушения', 'bilge portable hose');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Регулятор температуры прямого действия', 'Thermostatic control valve');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Сетка приемная', 'suction strainer');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Соединение гибкое', 'Flexible hose');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Соединение фланцевое с заглушкой международного образца', 'International type flange connection');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Стандартный фланец для сливных соединений и глухой фланец', 'standard flange for discharge connections and blind flange');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Устройство отбора проб', 'Sampling device');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фильтр', 'Strainer');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фильтр забортной воды', 'Sea water filter');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фильтр забортной воды опреснительной установки', 'Fresh water generator sea water filter');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фильтр паровой', 'Steam filter');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фильтр питьевой воды', 'Potable water filter');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фильтр сдвоенный смазочного масла', 'Lube oil duplex filter');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фильтр смазочного масла', 'Lube oil filter');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фильтр топливный', 'Fuel filter');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фильтр холодоносителя', 'Chilled water filte');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Фонарь смотровой', 'Sight glass');
INSERT INTO `fitting_type` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'Щелевой приемник', 'suction basket');

COMMIT;


-- -----------------------------------------------------
-- Data for table `conductor`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'гудрон', 'Goudron');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'дизельное топливо', 'diesel oil');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'забортная вода ', 'sea water');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'забортная вода c нефетпродуктами', 'sea water with oil');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'забортная вода, рассол', 'sea water, salt water');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'забортная вода/ пенообразователь ', 'sea water/ foam concentrat');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'нефтепродукты', 'oil products');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'охлажденная пресная вода', 'chilled water');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'пар', 'Steam');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'пар и конденсат ', 'Steam and condensate');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'пенообразователь ', 'Foam concentrat');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'пресная вода ', 'fresh water');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'сжатый воздух', 'compressed air');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'смазочное масло', 'lube oil');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'сточные воды', 'sewage water');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'термальное масло', 'Thermal oil');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'топливо', 'fuel oil');
INSERT INTO `conductor` (`id`, `name_rus`, `name_eng`) VALUES (DEFAULT, 'углекислота', 'Carbon dioxide');

COMMIT;

