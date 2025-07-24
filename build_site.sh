#!/bin/bash
set -e

printf "[*] Ensure output directory is empty\n"
rm -rf site_output
mkdir site_output


printf "[*] Copy over external code files\n"
cp -rv src/site/external site_output


printf "\n[*] Copy over static files\n"
cp -rv src/site/data site_output
cp -v src/site/survey.md site_output


printf "\n[*] Copy over styles\n"
cp -v src/site/*.css site_output


printf "\n[*] Copy over code files\n"
cp -v src/site/*.js site_output


printf "\n[*] Copy over html files\n"
cp -v src/site/index.html site_output
cp -v src/site/survey.html site_output
cp -v src/site/case_studies.html site_output


printf "\n[*] Generate chart images\n"
cd src/ChartGenerator
pnpm node main.js > /dev/null
cd ../..

cp -rv src/ChartGenerator/charts site_output
