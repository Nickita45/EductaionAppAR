-- --------------------------------------------------------
-- Хост:                         213.130.1.227
-- Версия сервера:               10.3.11-MariaDB - MariaDB Server
-- Операционная система:         Linux
-- HeidiSQL Версия:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Дамп структуры базы данных ar_school
CREATE DATABASE IF NOT EXISTS `ar_school` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `ar_school`;

-- Дамп структуры для таблица ar_school.answers
CREATE TABLE IF NOT EXISTS `answers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `q_id` int(11) DEFAULT NULL,
  `text` varchar(512) DEFAULT NULL,
  `correct` tinyint(1) DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица ar_school.questions
CREATE TABLE IF NOT EXISTS `questions` (
  `ID_question` int(11) NOT NULL AUTO_INCREMENT,
  `name_question` mediumtext DEFAULT NULL,
  `type_question` varchar(20) DEFAULT 'text',
  `type_answers` varchar(20) DEFAULT 'text',
  `variants` mediumtext DEFAULT NULL,
  `answer` mediumtext DEFAULT NULL,
  `id_tests` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID_question`),
  KEY `id_tests` (`id_tests`),
  CONSTRAINT `questions_ibfk_1` FOREIGN KEY (`id_tests`) REFERENCES `tests` (`ID_tests`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица ar_school.roles
CREATE TABLE IF NOT EXISTS `roles` (
  `role_id` tinyint(4) NOT NULL,
  `RoleName` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
INSERT into roles VALUES (0,"Admin");
INSERT into roles VALUES (1,"Student");
INSERT into roles VALUES (2,"Teacher");
-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица ar_school.statistics
CREATE TABLE IF NOT EXISTS `statistics` (
  `id_statistics` int(11) NOT NULL AUTO_INCREMENT,
  `id_student` int(11) DEFAULT NULL,
  `id_tests` int(11) DEFAULT NULL,
  `first_mark` text DEFAULT NULL,
  `another_mark` text DEFAULT NULL,
  PRIMARY KEY (`id_statistics`),
  KEY `id_student` (`id_student`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица ar_school.student
CREATE TABLE IF NOT EXISTS `student` (
  `id_student` int(11) NOT NULL,
  `groupNumber` varchar(20) DEFAULT NULL,
  `id_statistics` int(11) DEFAULT NULL,
  `id_teacher` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_student`),
  KEY `id_teacher` (`id_teacher`),
  CONSTRAINT `student_ibfk_1` FOREIGN KEY (`id_teacher`) REFERENCES `teacher` (`id_teacher`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица ar_school.teacher
CREATE TABLE IF NOT EXISTS `teacher` (
  `id_teacher` int(11) NOT NULL,
  `courseName` text DEFAULT NULL,
  PRIMARY KEY (`id_teacher`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица ar_school.tests
CREATE TABLE IF NOT EXISTS `tests` (
  `ID_tests` int(11) NOT NULL AUTO_INCREMENT,
  `id_teacher` int(11) DEFAULT NULL,
  `nameMain` text DEFAULT NULL,
  `nameCourse` text DEFAULT NULL,
  `resources_src` text DEFAULT NULL,
  PRIMARY KEY (`ID_tests`),
  KEY `id_teacher` (`id_teacher`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица ar_school.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(128) DEFAULT NULL,
  `passwd` varchar(128) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `role_id` tinyint(4) DEFAULT 1,
  `last_name` varchar(40) DEFAULT NULL,
  `school_name` varchar(20) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- Экспортируемые данные не выделены.

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
