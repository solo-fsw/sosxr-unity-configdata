# Changelog

All notable changes to this project will be documented in this file.
The changelog format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

## [2.3.4] -- in progress
### Fixed
- More control over which variables will be saved in the JSON file
- 

## [2.3.2] - 2025-02-10
### Fixed
- Fixed a bug where the ConfigData would not store the JSON when changing the value of a ConfigData in the Inspector


## [2.3.1] - 2025-02-06
### Fixed
- Bugs


## [2.3.0] - 2025-02-06
### Added 
- Added a PubSub system for the ConfigData

### Updated
- Updated the samples to include the PubSub system
- Updated Readme to include the PubSub system


## [2.2.1] - 2025-02-05
### Added
- Auto-delete the JSON file when the ConfigData is deleted from the project
- Logs when the JSON file is created, loaded, or deleted

### Changed
- Namespace for the samples 
- Reverted naming of the json file back to `config.json` since it was causing weird issues


## [2.2.0] - 2025-02-04
### Added
- Easy to use classes for getting (types of) data from the Config, which in turn trigger corresponding UnityEvents
- Wrapper for displaying Config in Hierarchy
- Namespaces

### Changed
- Improved the Samples
- ConfigData are stored by the name set in the Inspector


## [2.1.0] - 2025-01-31
### Added 
- Added samples as a separate thing: download them through the 'Samples' button in the package manager


## [2.0.0] - 2025-01-31
### Changed
- Changed from GNU GPL 3 license to MIT license


## [1.0.0] - 2025-01-28
### Added
- Initial release of the ConfigData package.