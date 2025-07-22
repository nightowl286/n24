# Ensure output directory is empty
rm -rf site_output
mkdir site_output


# Copy over external code files
cp -r src/site/external site_output


# Copy over static files
cp -r src/site/data site_output
cp src/site/survey.md site_output


# Copy over styles
cp src/site/*.css site_output


# Copy over code files
cp src/site/*.js site_output


# Copy over html files
cp src/site/index.html site_output
cp src/site/survey.html site_output
cp src/site/case_studies.html site_output
