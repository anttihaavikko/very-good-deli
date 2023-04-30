COLOR='\033[0;36m'
NC='\033[0m' # No Color

echo " "

echo "${COLOR}Pushing build for Windows${NC}"
butler push Builds/Win anttihaavikko/very-good-deli:win --fix-permissions

echo "${COLOR}Pushing build for OSX${NC}"
butler push Builds/macOS anttihaavikko/very-good-deli:osx --fix-permissions

echo "${COLOR}Pushing build for Linux${NC}"
butler push Builds/Linux anttihaavikko/very-good-deli:linux

# echo "${COLOR}Copying html5 files over to correct path"
# cp -a Builds/webgl/Html5/Build/. Builds/html5/Build
# echo "${COLOR}Pushing build for HTML5${NC}"
# butler push Builds/Html5 anttihaavikko/very-good-deli:html5
