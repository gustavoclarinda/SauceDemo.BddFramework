#!/usr/bin/env bash
set -euo pipefail

PROJECT="SauceDemo.BddFramework.csproj"
CONFIGURATION="Debug"
OUTPUT_DIR="TestReports"
RESULTS_DIR="$OUTPUT_DIR/allure-results"
REPORT_DIR="$OUTPUT_DIR/allure-report"
TEST_RESULTS_DIR="$OUTPUT_DIR/TestResults"

while [[ $# -gt 0 ]]; do
  case "$1" in
    --configuration|-c)
      CONFIGURATION="$2"
      shift 2
      ;;
    --output|-o)
      OUTPUT_DIR="$2"
      RESULTS_DIR="$OUTPUT_DIR/allure-results"
      REPORT_DIR="$OUTPUT_DIR/allure-report"
      TEST_RESULTS_DIR="$OUTPUT_DIR/TestResults"
      shift 2
      ;;
    *)
      echo "Unknown argument: $1" >&2
      exit 1
      ;;
  esac
done

ASSEMBLY_PATH="SauceDemo.BddFramework/bin/${CONFIGURATION}/net8.0/SauceDemo.BddFramework.dll"

rm -rf "$RESULTS_DIR" "$REPORT_DIR" "$TEST_RESULTS_DIR"
mkdir -p "$RESULTS_DIR" "$REPORT_DIR" "$TEST_RESULTS_DIR"

dotnet test "$PROJECT" \
  --configuration "$CONFIGURATION" \
  --logger "trx;LogFileName=TestResults.trx" \
  --results-directory "$TEST_RESULTS_DIR"

if ! command -v allure >/dev/null 2>&1; then
  echo "Allure CLI is required. Install it via Chocolatey (Windows), Homebrew (macOS) or npm, then re-run the script." >&2
  exit 1
fi

allure generate "$RESULTS_DIR" -o "$REPORT_DIR" --clean

echo "Allure report generated at: $REPORT_DIR/index.html"
