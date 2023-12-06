-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- ホスト: 127.0.0.1
-- 生成日時: 2023-12-01 12:26:52
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
-- テーブルの構造 `score_list`
--

CREATE TABLE `score_list` (
  `score_id` bigint(20) NOT NULL,
  `user_name` varchar(30) NOT NULL,
  `score` int(11) NOT NULL,
  `kill_cnt` int(11) DEFAULT NULL,
  `kill_score` int(11) DEFAULT NULL,
  `item_cnt` int(11) DEFAULT NULL,
  `item_score` int(11) DEFAULT NULL,
  `time_score` int(22) DEFAULT NULL,
  `score_date` datetime DEFAULT current_timestamp(),
  `remarks` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- テーブルのデータのダンプ `score_list`
--

INSERT INTO `score_list` (`score_id`, `user_name`, `score`, `kill_cnt`, `kill_score`, `item_cnt`, `item_score`, `time_score`, `score_date`, `remarks`) VALUES
(1, 'test1_player', 9999, 99, 990, 99, 990, 510, '2023-11-28 20:21:47', NULL),
(2, 'test1_player', 9990, 99, 990, 99, 990, 530, '2023-11-28 20:34:21', NULL),
(3, 'test1_player', 770, 77, 770, 77, 770, 500, '2023-11-28 20:57:06', NULL),
(4, 'test1_player', 1100, 11, 110, 11, 110, 100, '2023-11-28 20:57:31', NULL),
(5, 'test_player', 7770, 77, 770, 88, 880, 800, '2023-11-28 21:00:55', NULL),
(6, 'test1_player', 5500, 55, 550, 55, 550, 900, '2023-11-29 16:48:34', NULL);

-- --------------------------------------------------------

--
-- テーブルの構造 `user_list`
--

CREATE TABLE `user_list` (
  `user_id` bigint(20) NOT NULL,
  `user_name` varchar(30) NOT NULL,
  `user_pass` varchar(30) DEFAULT NULL,
  `created_date` datetime DEFAULT current_timestamp(),
  `remarks` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- テーブルのデータのダンプ `user_list`
--

INSERT INTO `user_list` (`user_id`, `user_name`, `user_pass`, `created_date`, `remarks`) VALUES
(1, 'test_player', '1234', '2023-11-28 19:14:16', NULL),
(38, 'test1_player', '1234', '2023-11-28 20:13:54', NULL),
(39, 'test2_player', '1234', '2023-11-28 20:13:58', NULL),
(40, 'test3_player', '1234', '2023-11-28 20:14:01', NULL),
(41, 'test4_player', '1234', '2023-11-28 20:14:04', NULL);

--
-- ダンプしたテーブルのインデックス
--

--
-- テーブルのインデックス `score_list`
--
ALTER TABLE `score_list`
  ADD PRIMARY KEY (`score_id`),
  ADD KEY `user_name` (`user_name`);

--
-- テーブルのインデックス `user_list`
--
ALTER TABLE `user_list`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `user_name` (`user_name`);

--
-- ダンプしたテーブルの AUTO_INCREMENT
--

--
-- テーブルの AUTO_INCREMENT `score_list`
--
ALTER TABLE `score_list`
  MODIFY `score_id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- テーブルの AUTO_INCREMENT `user_list`
--
ALTER TABLE `user_list`
  MODIFY `user_id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=173;

--
-- ダンプしたテーブルの制約
--

--
-- テーブルの制約 `score_list`
--
ALTER TABLE `score_list`
  ADD CONSTRAINT `score_list_ibfk_1` FOREIGN KEY (`user_name`) REFERENCES `user_list` (`user_name`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
