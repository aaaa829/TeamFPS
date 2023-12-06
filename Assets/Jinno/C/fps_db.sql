-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- ホスト: 127.0.0.1
-- 生成日時: 2023-11-22 12:20:34
-- サーバのバージョン： 10.4.28-MariaDB
-- PHP のバージョン: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- データベース: `fps_db`
--

-- --------------------------------------------------------

--
-- テーブルの構造 `player_list`
--

CREATE TABLE `player_list` (
  `player_id` bigint(20) UNSIGNED NOT NULL,
  `player_name` varchar(30) DEFAULT NULL,
  `player_pass` varchar(30) DEFAULT NULL,
  `created_date` datetime NOT NULL,
  `remarks` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- テーブルのデータのダンプ `player_list`
--

INSERT INTO `player_list` (`player_id`, `player_name`, `player_pass`, `created_date`, `remarks`) VALUES
(7, 'test_player', '1234', '2023-11-22 20:20:00', NULL),
(8, 'test1_player', '1234', '2023-11-22 20:20:00', NULL),
(10, 'test2_player', '1234', '2023-11-22 20:30:00', NULL),
(11, 'test3_player', '1234', '2023-11-22 20:40:00', NULL);

-- --------------------------------------------------------

--
-- テーブルの構造 `score_list`
--

CREATE TABLE `score_list` (
  `score_id` bigint(20) UNSIGNED NOT NULL,
  `player_name` varchar(30) DEFAULT NULL,
  `score` int(11) DEFAULT NULL,
  `item_score` int(11) DEFAULT NULL,
  `defeated_score` int(11) DEFAULT NULL,
  `time_score` time DEFAULT NULL,
  `score_date` datetime DEFAULT NULL,
  `remarks` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- テーブルのデータのダンプ `score_list`
--

INSERT INTO `score_list` (`score_id`, `player_name`, `score`, `item_score`, `defeated_score`, `time_score`, `score_date`, `remarks`) VALUES
(10, 'test2_player', 777, NULL, NULL, '00:02:00', '0000-00-00 00:00:00', NULL),
(25, 'test1_player', 888, NULL, NULL, '00:03:00', '0000-00-00 00:00:00', NULL),
(27, 'test2_player', 888, NULL, NULL, '00:03:00', '0000-00-00 00:00:00', NULL),
(28, 'test1_player', 888, NULL, NULL, '00:03:00', '2023-11-22 20:00:00', NULL),
(31, 'test_player', 111, NULL, NULL, '00:03:00', '2023-11-22 20:10:00', NULL);

--
-- ダンプしたテーブルのインデックス
--

--
-- テーブルのインデックス `player_list`
--
ALTER TABLE `player_list`
  ADD PRIMARY KEY (`player_id`);

--
-- テーブルのインデックス `score_list`
--
ALTER TABLE `score_list`
  ADD PRIMARY KEY (`score_id`);

--
-- ダンプしたテーブルの AUTO_INCREMENT
--

--
-- テーブルの AUTO_INCREMENT `player_list`
--
ALTER TABLE `player_list`
  MODIFY `player_id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- テーブルの AUTO_INCREMENT `score_list`
--
ALTER TABLE `score_list`
  MODIFY `score_id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
